import React, { useEffect, useState, useContext } from "react";
import "../App.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUsersViewfinder } from "@fortawesome/free-solid-svg-icons";
import axios from "axios";
import { v4 as uuid } from "uuid";
import { AuthContext } from "../provider/Auth";
import { AdminContext } from "../provider/Admin";
import { RoomContext } from "../provider/Room";

export default function Salas({ grupo, isAdmin, JoinRoom, closeConnection }) {
	const [salas, setSalas] = useState([]);
	const user = useContext(AuthContext);
	const ID_USER = String(user.user);
	const NOME_USER = String(user.userName);

	const { setAdmin } = useContext(AdminContext);
	const { setRoom } = useContext(RoomContext);
	const { setRoomName } = useContext(RoomContext);

	const handleClick = (e, m) => {
		e.preventDefault();
		setAdmin({ adm: isAdmin });
		setRoom(m.Id);
		closeConnection();
		JoinRoom(ID_USER, NOME_USER, m.Nome);
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
		<>
			{salas.map((m) =>
				grupo == m.Id ? (
					<a
						href='/'
						key={uuid()}
						onClick={(e) => {
							handleClick(e, m);
						}}
						className='relative rounded-lg px-2 py-2 flex items-center space-x-3 hover:border-gray-600 focus-within:ring-2 focus-within:ring-offset-2 focus-within:ring-offset-red-500 mb-3'
					>
						<div className='flex-shrink-0 no-underline'>
							<FontAwesomeIcon icon={faUsersViewfinder} size='lg' />
						</div>
						<div className='flex-1 min-w-0'>
							<div className='d-flex w-100 justify-content-between'>
								<p className='text-sm font-bold text-red-600 no-underline'>
									{m.Nome}
								</p>
							</div>
							<small className='text-sm text-gray-500 truncate no-underline'>
								{salaAtiva(m.isAtiva)}
							</small>
						</div>
					</a>
				) : (
					""
				)
			)}
		</>
	);
}
