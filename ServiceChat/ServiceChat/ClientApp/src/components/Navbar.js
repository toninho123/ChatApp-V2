import React, { useContext } from "react";
import { v4 as uuid } from "uuid";
import { RoomContext } from "../provider/Room";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Dropdown from "./Dropdown";
import { faAlignJustify } from "@fortawesome/free-solid-svg-icons";

export default function Navbar(props) {
	const room = useContext(RoomContext);

	const NOME_SALA = String(room.roomName);

	return (
		<>
			{room.roomName !== "" ? (
				<nav key={uuid()} className='flex-shrink-0 bg-red-600'>
					<div className='max-w-7xl mx-auto px-2 sm:px-4 lg:px-8'>
						<div className='relative flex items-center h-16'>
							<div className='flex lg:hidden'></div>
							<div className='lg:block lg:w-96'>
								<div className='flex items-center'>
									<div className='flex'>
										<div className='px-3 py-2 rounded-md text-sm font-medium text-white hover:text-white'>
											{NOME_SALA}
										</div>
									</div>
									<div className='ml-4 relative flex-shrink-0'>
										<div>
											<button className='bg-red-700 flex text-sm rounded-full text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-red-700 focus:ring-white'></button>
										</div>
									</div>
								</div>
							</div>
							<div className='lg:block lg:w-80 ml-[900px]'>
								<div className='flex items-center'>
									<div className='flex'>
										<Dropdown />
									</div>
									<div className='ml-4 relative flex-shrink-0'>
										<div>
											<button className='bg-red-700 flex text-sm rounded-full text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-red-700 focus:ring-white'></button>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</nav>
			) : (
				<nav key={uuid()} className='flex-shrink-0 bg-red-600'>
					<div className='max-w-7xl mx-auto px-2 sm:px-4 lg:px-8'>
						<div className='relative flex items-center h-16'>
							<div></div>
							<div className='flex lg:hidden'></div>
							<div className='lg:block lg:w-96'>
								<div className='flex items-center'>
									<div className='flex'>
										<div className='px-3 py-2 rounded-md text-sm font-medium text-white hover:text-white'>
											A Carregar...
										</div>
									</div>
									<div className='ml-4 relative flex-shrink-0'>
										<div>
											<button className='bg-red-700 flex text-sm rounded-full text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-red-700 focus:ring-white'></button>
										</div>
									</div>
								</div>
							</div>
							<div className='lg:block lg:w-80 ml-[900px]'>
								<div className='flex items-center'>
									<div className='flex'>
										<Dropdown />
									</div>
									<div className='ml-4 relative flex-shrink-0'>
										<div>
											<button className='bg-red-700 flex text-sm rounded-full text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-red-700 focus:ring-white'></button>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</nav>
			)}
		</>
	);
}
