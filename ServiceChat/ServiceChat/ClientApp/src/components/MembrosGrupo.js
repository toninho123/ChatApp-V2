import React, { useEffect, useState, useContext, useRef } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { Dialog, Transition } from "@headlessui/react";
import axios from "axios";
import { v4 as uuid } from "uuid";
import { AuthContext } from "../provider/Auth";
import { AdminContext } from "../provider/Admin";
import { RoomContext } from "../provider/Room";

export default function MembrosGrupo(props) {
	const [utilizadores, setUtilizadores] = useState([]);
	const user = useContext(AuthContext);
	const admin = useContext(AdminContext);
	const room = useContext(RoomContext);
	const cancelButtonRef = useRef(null);

	useEffect(() => {
		if (room.room)
			axios
				.get("api/utilizador", {
					params: {
						id: room.room,
					},
				})
				.then((res) => {
					setUtilizadores(res.data);
				});
	}, []);

	const handleClick = (e) => {
		console.log(e);
		axios
			.delete("api/utilizador/" + e)
			.then(() => console.log("Utilizador removido com sucesso"));
	};

	return (
		<Transition.Root show={props.open}>
			<Dialog
				as='div'
				className='relative z-10'
				initialFocus={cancelButtonRef}
				onClose={props.setOpen}
			>
				<Transition.Child
					enter='ease-out duration-300'
					enterFrom='opacity-0'
					enterTo='opacity-100'
					leave='ease-in duration-200'
					leaveFrom='opacity-100'
					leaveTo='opacity-0'
				>
					<div className='fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity' />
				</Transition.Child>

				<div className='fixed inset-0 z-10 overflow-y-auto'>
					<div
						onClick={() => {
							props.setOpen(false);
						}}
						className='flex min-h-full items-end justify-center p-4 text-center sm:items-center sm:p-0'
					>
						<Transition.Child
							enter='ease-out duration-300'
							enterFrom='opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95'
							enterTo='opacity-100 translate-y-0 sm:scale-100'
							leave='ease-in duration-200'
							leaveFrom='opacity-100 translate-y-0 sm:scale-100'
							leaveTo='opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95'
						>
							<Dialog.Panel className='relative transform overflow-hidden rounded-lg bg-white text-left shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-4xl'>
								<div class='overflow-x-auto relative shadow-md sm:rounded-lg '>
									{admin.admin.adm === true ? (
										<table class='w-full text-sm text-left text-gray-500 ext-gray-400'>
											<thead class='text-xs text-gray-700 uppercase bg-gray-50'>
												<tr>
													<th scope='col' class='py-3 px-6'>
														Id
													</th>
													<th scope='col' class='py-3 px-6'>
														Nome
													</th>
													<th scope='col' class='py-3 px-6'>
														Numero
													</th>
													<th scope='col' class='py-3 px-6'>
														Função
													</th>
													<th scope='col' class='py-3 px-6'>
														Estado
													</th>
													<th scope='col' class='py-3 px-6'>
														<span class='sr-only'>Editar</span>
													</th>
												</tr>
											</thead>
											<tbody>
												{utilizadores.map((e) => (
													<tr
														key={uuid()}
														class='bg-gray-300 border-b hover:bg-gray hover:bg-opacity-50 '
													>
														<td class='text-gray-900 py-4 px-6'>{e.Id}</td>
														<td class='text-gray-900 py-4 px-6'>
															{e.nomeUtilizador}
														</td>
														<td class='text-gray-900 py-4 px-6'>
															{e.Numero_Aluno}
														</td>
														<td class='text-gray-900 text-gray-900py-4 px-6'>
															{e.Funcao}
														</td>
														<td class='text-gray-900 py-4 px-6'>{e.Estado}</td>
														{user.user == e.Id ? (
															<td></td>
														) : (
															<td class='text-gray-900 py-4 px-6 text-right'>
																<button
																	onClick={() => handleClick(e.Id)}
																	className='h-8 px-4 m-2 text-sm text-indigo-100 transition-colors duration-150 bg-red-600 rounded-lg focus:shadow-outline hover:bg-red-700'
																>
																	<FontAwesomeIcon icon={faXmark} />
																</button>
															</td>
														)}
													</tr>
												))}
											</tbody>
										</table>
									) : (
										<table class='w-full text-sm text-left text-gray-500 ext-gray-400'>
											<thead class='text-xs text-gray-700 uppercase bg-gray-50'>
												<tr>
													<th scope='col' class='py-3 px-6'>
														Id
													</th>
													<th scope='col' class='py-3 px-6'>
														Nome Utilizador
													</th>
													<th scope='col' class='py-3 px-6'>
														Numero
													</th>
													<th scope='col' class='py-3 px-6'>
														Função
													</th>
													<th scope='col' class='py-3 px-6'>
														Estado
													</th>
												</tr>
											</thead>
											<tbody>
												{utilizadores.map((e) => (
													<tr
														key={uuid()}
														class='bg-gray-300 border-b hover:bg-gray hover:bg-opacity-50 '
													>
														<td class='text-gray-900 py-4 px-6'>{e.Id}</td>
														<td class='text-gray-900 py-4 px-6'>
															{e.nomeUtilizador}
														</td>
														<td class='text-gray-900 py-4 px-6'>
															{e.Numero_Aluno}
														</td>
														<td class='text-gray-900 text-gray-900py-4 px-6'>
															{e.Funcao}
														</td>
														<td class='text-gray-900 py-4 px-6'>{e.Estado}</td>
														<td class='text-gray-900 py-4 px-6 text-right'></td>
													</tr>
												))}
											</tbody>
										</table>
									)}
								</div>
							</Dialog.Panel>
						</Transition.Child>
					</div>
				</div>
			</Dialog>
		</Transition.Root>
	);
}
