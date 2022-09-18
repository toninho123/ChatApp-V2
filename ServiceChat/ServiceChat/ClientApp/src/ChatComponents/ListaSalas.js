import React, { useEffect, useContext, useState } from "react";
import Procurar from "./Procurar";
import Salas from "./Salas";
import axios from "axios";
import { AuthContext } from "../provider/Auth";
import { RoomContext } from "../provider/Room";

export default function ListaSalas(props) {
	const [grupo, setGrupo] = useState([]);
	const [filtro, setFiltro] = useState("");
	const user = useContext(AuthContext);
	const { setRoom } = useContext(RoomContext);
	const { setRoomName } = useContext(RoomContext);

	useEffect(() => {
		axios
			.get("api/grupo", {
				params: {
					id: user.user,
				},
			})
			.then(function (response) {
				setGrupo(response.data);
				if (response.data.length) {
					setRoom(response.data[0].Id);
					setRoomName(response.data[0].salaNome);
					localStorage.setItem("nomeSala", response.data[0].salaNome);
				}
			});
	}, []);

	return (
		<div className='border-b border-gray-200 xl:border-b-0 xl:flex-shrink-0 xl:w-64 xl:border-r xl:border-gray-600 bg-gray-500'>
			<div className='h-full pl-4 pr-2 py-6 sm:pl-6 lg:pl-8 xl:pl-0'>
				<div className='h-full relative'>
					<Procurar search={filtro} setSearch={setFiltro} />
					{grupo
						.filter((x) => {
							if (filtro === "") {
								return x;
							} else if (x.nomeGrupo.includes(filtro)) {
								return x;
							}
						})
						.map((m) => (
							<div key={m.Id}>
								<Salas
									grupo={m.Id_Sala}
									isAdmin={m.Administrador}
									JoinRoom={props.JoinRoom}
									closeConnection={props.closeConnection}
								/>
								{m.nomeGrupo}
							</div>
						))}
				</div>
			</div>
		</div>
	);
}
