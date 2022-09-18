import React, { useEffect, useState, useContext } from "react";
import axios from "axios";
import { v4 as uuid } from "uuid";
import { AuthContext } from "../provider/Auth";
import { AdminContext } from "../provider/Admin";
import { RoomContext } from "../provider/Room";

export default function Salas({ grupo, isAdmin, JoinRoom, closeConnection }) {
	const [salas, setSalas] = useState([]);
	const user = useContext(AuthContext);

	const NOME_USER = String(user.user);

	const { setAdmin } = useContext(AdminContext);
	const { setRoom } = useContext(RoomContext);
	const { setRoomName } = useContext(RoomContext);

	const handleClick = (e, m) => {
		e.preventDefault();
		setAdmin({ adm: isAdmin });
		setRoom(m.Id);
		closeConnection();
		JoinRoom(NOME_USER, "Ze", m.Nome);
		setRoomName(m.Nome);
	};

	const salaAtiva = (ativa) => {
		if (ativa) return "Ativa";
		else return "Inativa";
	};

	useEffect(() => {
		axios.get("api/sala").then(function (response) {
			setSalas(response.data);
		});
	}, []);

	return (
		<div className='relative rounded-lg px-2 py-2 flex items-center space-x-3 hover:border-gray-600 focus-within:ring-2 focus-within:ring-offset-2 focus-within:ring-offset-red-500 mb-4'>
			{salas.map((m) => {
				console.log(grupo);
				grupo == m.Id ? (
					<div className='flex-1 min-w-0'>
						<a
							href='/'
							key={uuid()}
							className='focus:outline-none'
							onClick={(e) => {
								handleClick(e, m);
							}}
						>
							<span className='absolute inset-0' />
							<p className='text-sm font-bold text-red-600'>{m.Nome}</p>
							<p className='text-sm text-gray-500 truncate'>
								{salaAtiva(m.isAtiva)}
							</p>
						</a>
					</div>
				) : (
					""
				);
			})}
		</div>
	);
}
