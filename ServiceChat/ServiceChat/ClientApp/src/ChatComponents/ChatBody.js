import React, { useEffect, useState, useContext } from "react";
import axios from "axios";
import { v4 as uuid } from "uuid";
import { AuthContext } from "../provider/Auth";
import { RoomContext } from "../provider/Room";
import Mensagem from "./Mensagem";
import MensagemLink from "./MensagemLink";
import NavbarChat from "./NavbarChat";

export default function ChatBody({ messages }) {
	const [mensagem, setMensagem] = useState([]);

	const user = useContext(AuthContext);
	const room = useContext(RoomContext);

	console.log(user);

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

		if (message.user.id === 0) {
			message.user.id = user.user;
			message.user.nome = nomeDoUtilizador.Nome;
		}

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
						corTexto={"bg-gray-200"}
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
						corTexto={"bg-gray-200"}
						texto={obj.value}
						utilizador={nomeDoUtilizador.Nome}
					/>
				);
			}
		}
	};

	return (
		<>
			<NavbarChat />
			{/*MENSAGENS COMEÇAM AQUI*/}
			{mensagem.map((m) => (
				<div key={uuid()}>
					{m.Ficheiro === "" ? (
						<Mensagem
							key={uuid()}
							classe={m.Id_Utilizador == user.user ? "justify-end" : ""}
							corTexto={
								m.Id_Utilizador == user.user ? "bg-red-400" : "bg-gray-200"
							}
							texto={m.Texto}
							utilizador={m.Id_Utilizador == user.user ? "" : m.Nome}
						/>
					) : (
						<MensagemLink
							key={uuid()}
							classe={m.Id_Utilizador == user.user ? "justify-end" : ""}
							corTexto={
								m.Id_Utilizador == user.user ? "bg-red-400" : "bg-gray-200"
							}
							texto={m.Texto}
							file={m.Ficheiro}
							utilizador={m.Id_Utilizador == user.user ? "" : m.Nome}
						/>
					)}
				</div>
			))}
			{messages &&
				messages.map((m) => <div key={uuid()}>{buildMessage(m)}</div>)}
			{/*MENSAGENS ACABAM AQUI*/}
		</>
	);
}
