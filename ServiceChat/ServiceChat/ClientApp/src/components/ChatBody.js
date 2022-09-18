import React, { useEffect, useState, useContext } from "react";
import Mensagem from "./Mensagem";
import MensagemLink from "./MensagemLink";
import axios from "axios";
import Navbar from "./Navbar";
import "../App.css";
import { v4 as uuid } from "uuid";
import { AuthContext } from "../provider/Auth";
import { RoomContext } from "../provider/Room";

export default function ChatBody({ messages }) {
	const [mensagem, setMensagem] = useState([]);
	const user = useContext(AuthContext);
	const room = useContext(RoomContext);

	useEffect(() => {
		if (room.room > 0)
			axios
				.get("api/mensagem", {
					params: {
						id: room.room,
					},
				})
				.then(function (response) {
					setMensagem(response.data);
				});
	}, [room.room]);

	const buildMessage = (message) => {
		const obj =
			typeof message.message === "string"
				? JSON.parse(message.message)
				: message.message;
		const objUser = message.user;

		const nomeDoUtilizador = mensagem.find((e) => e.Id_Utilizador == user.user);

		if (obj.type === "link")
			if (objUser.id == user.user) {
				return (
					<MensagemLink
						classe={"justify-end"}
						file={obj.file}
						texto={obj.value}
						corTexto={"bg-red-400"}
					/>
				);
			} else {
				return (
					<MensagemLink
						texto={obj.value}
						file={obj.file}
						utilizador={nomeDoUtilizador.Nome}
						corTexto={"bg-blue-400"}
					/>
				);
			}
		else {
			if (objUser.id == user.user) {
				return (
					<Mensagem
						classe={"justify-end"}
						corTexto={"bg-red-400"}
						texto={obj.value}
					/>
				);
			} else {
				return (
					<Mensagem
						corTexto={"bg-blue-400"}
						texto={obj.value}
						utilizador={nomeDoUtilizador.Nome}
					/>
				);
			}
		}
	};

	return (
		<>
			<Navbar />
			<div className='overflow-auto'>
				{mensagem.map((m) => (
					<div key={uuid()} className='from-user'>
						{m.Ficheiro === "" ? (
							<Mensagem
								key={uuid()}
								classe={m.Id_Utilizador == user.user ? "justify-end" : ""}
								corTexto={
									m.Id_Utilizador == user.user ? "bg-red-400" : "bg-blue-400"
								}
								texto={m.Texto}
								utilizador={m.Id_Utilizador == user.user ? "" : m.Nome}
							/>
						) : (
							<MensagemLink
								key={uuid()}
								classe={m.Id_Utilizador == user.user ? "justify-end" : ""}
								corTexto={
									m.Id_Utilizador == user.user ? "bg-red-400" : "bg-blue-400"
								}
								texto={m.Texto}
								file={m.Ficheiro}
								utilizador={m.Id_Utilizador == user.user ? "" : m.Nome}
							/>
						)}
					</div>
				))}
				{messages &&
					messages.map((m) => (
						<div key={uuid()} className='from-user'>
							{buildMessage(m)}
						</div>
					))}
			</div>
		</>
	);
}
