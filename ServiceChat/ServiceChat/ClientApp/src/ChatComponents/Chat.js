import React, { useState } from "react";
import ListaSalas from "./ListaSalas";
import EnviarMensagem from "./EnviarMensagem";
import ChatBody from "./ChatBody";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

export default function Chat(props) {
	const [sala, setSala] = useState(0);
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
			console.log("MESSAGE TEST: ", message);
			await connection.invoke("SendMessage", JSON.stringify(message));
		} catch (error) {
			console.log(error);
		}
	}

	return (
		<div className='flex-grow w-full lg:flex bg-white xl:flex'>
			<div className='flex-1 min-w-0 bg-white xl:flex'>
				<ListaSalas JoinRoom={JoinRoom} closeConnection={closeConnection} />

				<div className='flex-1 p:2 sm:pb-6 justify-between flex flex-col h-screen xl:flex bg-gray-400'>
					<ChatBody messages={messages} />
					<EnviarMensagem sendMessage={sendMessage} />
				</div>
			</div>
		</div>
	);
}
