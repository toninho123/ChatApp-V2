import React, { useEffect, useContext, useState } from "react";
import Salas from "./Salas";
import Procurar from "./Procurar";
import CriarSala from "./CriarSala";
import axios from "axios";
import { AuthContext } from "../provider/Auth";
import { RoomContext } from "../provider/Room";

export default function ListaContactos(props) {
	const [grupo, setGrupo] = useState([]);
	const [getUtilizadores, setGetUtilizadores] = useState([]);
	const [triggerSala, setTriggerSala] = useState(true);
	const [filtro, setFiltro] = useState("");
	const [ativarCriarSala, setAtivarCriarSala] = useState(false);
	const user = useContext(AuthContext);
	const { setRoom } = useContext(RoomContext);
	const { setRoomName } = useContext(RoomContext);

	useEffect(() => {
		if (triggerSala)
			axios
				.get("api/chat_membros", {
					params: {
						id: localStorage.getItem("user"),
					},
				})
				.then(function (response) {
					setGrupo(response.data);
					if (response.data.length) {
						setRoom(response.data[0].Id);
						setRoomName(response.data[0].Nome);
						setTriggerSala(false);
						localStorage.setItem("room", response.data[0].Id);
						localStorage.setItem("roomName", response.data[0].Nome);
					}
				}),
				axios
					.get("api/criar_sala", {
						params: {
							id: localStorage.getItem("user"),
						},
					})
					.then(function (res) {
						setGetUtilizadores(res.data);
					});
	}, [triggerSala]);

	return (
		<div className='border-b border-gray-200 xl:border-b-0 xl:flex-shrink-0 xl:w-96 xl:border-r xl:border-black bg-gray-200'>
			<div className='h-full pl-4 pr-2 py-6 sm:pl-6 lg:pl-8 xl:pl-0 overflow-auto'>
				<button
					onClick={() => {
						switch (ativarCriarSala) {
							case true:
								setAtivarCriarSala(false);
								break;
							case false:
								setAtivarCriarSala(true);
								break;
						}
					}}
					className='inline-flex w-full justify-center rounded-md border border-gray-300 bg-red-500 px-4 py-2 text-sm font-medium text-gray-700 shadow-sm hover:bg-red-300 focus:outline-none focus:ring-2 focus:ring-gray-50 focus:ring-offset-2 focus:ring-offset-gray-100'
				>
					{ativarCriarSala ? "Finalizar" : "Criar Sala"}
				</button>
				<div className='h-full relative'>
					{ativarCriarSala ? (
						<CriarSala
							getUtilizadores={getUtilizadores}
							setTriggerSala={setTriggerSala}
						/>
					) : (
						<>
							<Procurar search={filtro} setSearch={setFiltro} />

							{grupo
								.filter((x) => {
									if (filtro === "") {
										return x;
									} else if (x.Nome.includes(filtro)) {
										return x;
									}
								})
								.map((m) => (
									<div key={m.Id}>
										<Salas
											grupo={m.Id_Grupo}
											isAdmin={m.Administrador}
											JoinRoom={props.JoinRoom}
											closeConnection={props.closeConnection}
										/>
									</div>
								))}
						</>
					)}
				</div>
			</div>
		</div>
	);
}
