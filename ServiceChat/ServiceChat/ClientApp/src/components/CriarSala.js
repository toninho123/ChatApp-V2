import React, { useState, useEffect } from "react";
import Select from "react-select";
import axios from "axios";

export default function CriarSala({ getUtilizadores, setTriggerSala }) {
	const [selectedOptions, setSelectedOptions] = useState([]);
	const [limite, setLimite] = useState(0);
	const [salas, setSalas] = useState([]);

	const handleSelect = () => {
		return selectedOptions.map((e) => e.value);
	};

	const testarLimite = () => {
		if (limite >= 2) console.log("AGORA CARREGAR MENSAGEM PARA FAZER SALA");
	};

	useEffect(() => {
		// axios.get("api/chat_grupos").then(function (response) {
		// 	setSalas(response.data);
		// });
		carregarSalas();
	}, []);

	const carregarSalas = () => {
		axios.get("api/chat_membros").then(function (response) {
			setSalas(response.data);
		});
	};

	const adicionarUtilizadores = () => {
		const formData = new FormData();
		try {
			axios
				.post("api/chat_grupos", {
					Nome: "TRIGER_TESTE",
					Id_Curso: 1,
					Id_Entidade: 1,
					Capa: "CapaX",
					Ativo: false,
					Tipo: 0,
				})
				.then((response) => {
					const IdSala = response.data;

					const objteste = { value: Number(localStorage.getItem("user")) };
					selectedOptions.unshift(objteste);

					//const Ids = selectedOptions.map((e) => e.value);

					selectedOptions.forEach((e) => {
						let isAdmin = 0;

						if (e.value === objteste.value) isAdmin = 1;

						axios
							.post("/api/criar_sala", {
								Administrador: isAdmin,
								Id_Grupo: IdSala,
								Id_Utilizador: e.value,
							})
							.then((response) => {
								console.log(response);
								setTriggerSala(true);
							})
							.catch((response) => console.log("error", response));
					});
				})
				.catch((response) => console.log("error", response));

			/*axios
				.post("/api/criar_sala", { Ids, IdSala })
				.then((response) => {
					console.log(response);
				})
				.catch((response) => console.log("error", response));*/
		} catch (ex) {
			console.log(ex);
		}
	};

	return (
		<>
			<Select
				isMulti
				options={getUtilizadores}
				onChange={(item) => {
					setSelectedOptions(item);
					setLimite(item.length);
				}}
			/>

			<button onClick={adicionarUtilizadores}>Imprimir Items</button>
		</>
	);
}
