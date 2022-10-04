import React, { useState, useEffect } from "react";
import Select from "react-select";
import axios from "axios";

export default function CriarSala({ getUtilizadores }) {
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
		axios.get("api/chat_grupos").then(function (response) {
			setSalas(response.data);
		});
	}, []);

	const adicionarUtilizadores = () => {
		const formData = new FormData();
		try {
			/*axios.post("api/chat_grupos", {
				Nome: "novasalateste",
				Id_Curso: 1,
				Id_Entidade: 1,
				Capa: "CapaX",
				Ativo: false,
				Tipo: 0,
			});*/

			const objteste = { value: Number(localStorage.getItem("user")) };
			selectedOptions.unshift(objteste);

			formData.append("Ids", Number(selectedOptions.map((e) => e.value)));
			formData.append("IdSala", Number(salas[salas.length - 1].Id));

			const json = JSON.stringify({
				Ids: selectedOptions.map((e) => Number(e.value)),
				IdSala: Number(salas[salas.length - 1].Id),
			});

			console.log(json);

			axios({
				method: "post",
				url: "/api/criar_sala",
				data: json,
				headers: {
					"Content-Type": "application/json; charset=UTF-8",
				},
			})
				.then((response) => {
					console.log(response);
				})
				.catch((response) => console.log("error", response));
		} catch (ex) {
			console.log(ex.response);
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
