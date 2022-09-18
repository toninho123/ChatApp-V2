import React, { useState } from "react";
import ListaContactos from "./ListaContactos";
import ChatBody from "./ChatBody";
import SendMessage from "./SendMessage";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

function Chat() {
	const [connection, setConnection] = useState();
	const [messages, setMessages] = useState([]);
	const [users, setusers] = useState([]);

	const JoinRoom = async (id, user, room) => {
		try {
			const connection = new HubConnectionBuilder()
				.withUrl("https://localhost:44344/chat")
				.configureLogging(LogLevel.Information)
				.build();

			connection.on("UsersInRoom", (users) => {
				setusers(users);
			});

			connection.on("ReceiveMessage", (user, message) => {
				console.log("receiving...");
				setMessages((messages) => [...messages, { user, message }]);
			});

			connection.onclose((e) => {
				setConnection();
				setMessages([]);
				setusers([]);
			});

			await connection.start();
			await connection.invoke("JoinRoom", { id, user, room });
			setConnection(connection);
		} catch (e) {
			console.log(e);
		}
	};

	async function closeConnection() {
		if (connection) {
			try {
				await connection.stop();
			} catch (error) {
				console.log(error);
			}
		} else return;
	}

	async function sendMessage(message) {
		try {
			await connection.invoke("SendMessage", JSON.stringify(message));
		} catch (error) {
			console.log(error);
		}
	}

	return (
		<>
			<ListaContactos JoinRoom={JoinRoom} closeConnection={closeConnection} />
			<div className='flex-1 p:2 sm:pb-6 justify-between flex flex-col h-screen xl:flex'>
				<ChatBody messages={messages} />
				<SendMessage sendMessage={sendMessage} />
			</div>
		</>
	);
}

export default Chat;
