import React from "react";
import "../App.css";
import Chat from "./Chat";
import { useState } from "react";

function Home() {
	const [connection, setConnection] = useState();
	const [messages, setMessages] = useState([]);
	const [users, setusers] = useState([]);

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
			await connection.invoke("SendMessage", message);
		} catch (error) {
			console.log(error);
		}
	}

	return (
		<div className='flex-grow w-full lg:flex bg-gray-50 xl:flex'>
			<div className='flex-1 min-w-0 bg-gray-500 bg-opacity-40 xl:flex'>
				<Chat
					messages={messages}
					sendMessages={sendMessage}
					closeConnection={closeConnection}
					users={users}
				/>
			</div>
		</div>
	);
}

export default Home;
