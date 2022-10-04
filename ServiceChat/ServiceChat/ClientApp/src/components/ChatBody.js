import React, { useEffect, useState, useContext } from "react";
import Mensagem from "./Mensagem";
import MensagemLink from "./MensagemLink";
import axios from "axios";
import Navbar from "./Navbar";
import "../App.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUserPlus } from "@fortawesome/free-solid-svg-icons";
import { v4 as uuid } from "uuid";
import { AuthContext } from "../provider/Auth";
import { RoomContext } from "../provider/Room";

export default function ChatBody({ messages }) {
	const [mensagem, setMensagem] = useState([]);
	const user = useContext(AuthContext);
	const room = useContext(RoomContext);

	useEffect(() => {
		if (localStorage.getItem("room") > 0)
			axios
				.get("api/chat_mensagens", {
					params: {
						id: localStorage.getItem("room"),
					},
				})
				.then(function (response) {
					setMensagem(response.data);
				});
	}, [localStorage.getItem("room")]);

	const buildMessage = (message) => {
		const obj =
			typeof message.message === "string"
				? JSON.parse(message.message)
				: message.message;
		const objUser = message.user;

		const nomeDoUtilizador = mensagem.find(
			(e) => e.Id_Utilizador == localStorage.getItem("user")
		);

		if (obj.type === "link")
			if (objUser.id == localStorage.getItem("user")) {
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
			if (objUser.id == localStorage.getItem("user")) {
				return (
					<Mensagem
						classe={"justify-end"}
						corTexto={"bg-red-400"}
						texto={obj.value}
						utilizadorId={objUser.id}
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
						{m.Anexo === "" ? (
							<Mensagem
								key={uuid()}
								classe={
									m.Id_Utilizador == localStorage.getItem("user")
										? "justify-end"
										: ""
								}
								corTexto={
									m.Id_Utilizador == localStorage.getItem("user")
										? "bg-red-400"
										: "bg-blue-400"
								}
								texto={m.Texto}
								utilizador={
									m.Id_Utilizador == localStorage.getItem("user") ? "" : m.Nome
								}
								addUtilizador={
									m.Id_Utilizador == localStorage.getItem("user") ? (
										""
									) : (
										<FontAwesomeIcon icon={faUserPlus} />
									)
								}
							/>
						) : (
							<MensagemLink
								key={uuid()}
								classe={
									m.Id_Utilizador == localStorage.getItem("user")
										? "justify-end"
										: ""
								}
								corTexto={
									m.Id_Utilizador == localStorage.getItem("user")
										? "bg-red-400"
										: "bg-blue-400"
								}
								texto={m.Texto}
								file={m.Ficheiro}
								utilizador={
									m.Id_Utilizador == localStorage.getItem("user") ? "" : m.Nome
								}
								addUtilizador={
									m.Id_Utilizador == localStorage.getItem("user") ? (
										""
									) : (
										<FontAwesomeIcon icon={faUserPlus} />
									)
								}
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
