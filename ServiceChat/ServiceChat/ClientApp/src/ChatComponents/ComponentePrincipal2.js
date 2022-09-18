import React, { useState } from "react";
import Chat from "./Chat";

export default function ComponentePrincipal2() {
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
		<div>
			<div>
				<div className='relative min-h-screen flex flex-col bg-gray-200'>
					<Chat
						messages={messages}
						sendMessages={sendMessage}
						closeConnection={closeConnection}
						users={users}
					/>
				</div>
			</div>
		</div>
	);
}
