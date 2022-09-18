import React, { useState, useContext } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEllipsisV } from "@fortawesome/free-solid-svg-icons";
import { v4 as uuid } from "uuid";
import { RoomContext } from "../provider/Room";

export default function NavbarChat(props) {
	const [show, setShow] = useState(false);
	const room = useContext(RoomContext);
	const buttonStyle =
		"'inline-flex items-center justify-center rounded-full h-10 w-10 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300 focus:outline-none'";

	const NOME_SALA = String(room.roomName);

	const handleClick = (e) => {
		setShow(true);
		e.preventDefault();
	};

	return (
		<>
			{room.roomName !== "" ? (
				<div
					key={uuid()}
					className='w-full sm:items-center justify-between  border-b border-gray-600'
				>
					<div className='flex items-center space-x-4'>
						<div className='flex flex-col leading-tight '>
							<div className='text-1xl mt-1 flex items-center'>
								<span className='text-gray-700 mr-3'>{NOME_SALA}</span>
							</div>
						</div>
					</div>

					<div className='flex items-center space-x-2'>
						<button className={buttonStyle} onClick={(e) => handleClick(e)}>
							<FontAwesomeIcon icon={faEllipsisV} />
						</button>
					</div>
				</div>
			) : (
				<div
					key={uuid()}
					className='w-full sm:items-center justify-between  border-b border-gray-600'
				>
					<div className='flex items-center space-x-4'>
						<div className='flex flex-col leading-tight '>
							<div className='text-1xl mt-1 flex items-center'>
								<span className='text-gray-700 mr-3'>A Carregar...</span>
							</div>
						</div>
					</div>

					<div className='flex items-center space-x-2'>
						<button className='inline-flex items-center justify-center rounded-full h-10 w-10 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300 focus:outline-none'>
							<FontAwesomeIcon icon={faEllipsisV} />
						</button>
					</div>
				</div>
			)}
		</>
	);
}
