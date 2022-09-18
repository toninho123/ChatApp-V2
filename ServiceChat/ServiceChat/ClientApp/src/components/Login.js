import React, { useState, useContext } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../provider/Auth";

export default function Login() {
	const [numeroUtilizador, setNumeroUtilizador] = useState("");
	const { setUser, setUserName } = useContext(AuthContext);

	const navigate = useNavigate();

	const handleSubmit = (e) => {
		e.preventDefault();
		axios.get("api/utilizador/" + numeroUtilizador).then((res) => {
			setUser(res.data[0].Id);
			setUserName(res.data[0].Nome);
			navigate("/hom");
		});
	};

	return (
		<div className='relative flex flex-col justify-center min-h-screen overflow-hidden bg-gray-500 bg-opacity-75'>
			<div className='w-full p-6 m-auto bg-gray-200 rounded-md shadow-md lg:max-w-xl'>
				<form
					className='mt-6'
					onSubmit={(e) => {
						handleSubmit(e);
					}}
				>
					<div className='mb-2'>
						<label className='block text-sm font-semibold text-gray-800'>
							Numero
						</label>
						<input
							className='block w-full px-4 py-2 mt-2 text-red-600 bg-white border rounded-md focus:border-red-400 focus:ring-red-300 focus:outline-none focus:ring focus:ring-opacity-40'
							value={numeroUtilizador}
							onChange={(e) => setNumeroUtilizador(e.target.value)}
						/>
					</div>
					<div className='mb-2'>
						<label className='block text-sm font-semibold text-gray-800'>
							Password
						</label>
						<input
							type='password'
							className='block w-full px-4 py-2 mt-2 text-red-600 bg-white border rounded-md focus:border-red-400 focus:ring-red-300 focus:outline-none focus:ring focus:ring-opacity-40'
						/>
					</div>
					<div className='mt-6'>
						<button
							type='submit'
							className='w-full px-4 py-2 tracking-wide text-white transition-colors duration-200 transform bg-red-600 rounded-md hover:bg-red-500 focus:outline-none focus:bg-red-500'
						>
							Login
						</button>
					</div>
				</form>
			</div>
		</div>
	);
}
