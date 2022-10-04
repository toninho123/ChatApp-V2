import React, { useState, useContext } from "react";
import axios from "axios";
import { AuthContext } from "../provider/Auth";
import { RoomContext } from "../provider/Room";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPaperclip } from "@fortawesome/free-solid-svg-icons";
import { faUpload } from "@fortawesome/free-solid-svg-icons";
import { faPaperPlane } from "@fortawesome/free-solid-svg-icons";

export default function SendMessage(props) {
	const [message, setMessage] = useState({});
	const [file, setFile] = useState();
	const [fileName, setFileName] = useState();
	const room = useContext(RoomContext);
	const user = useContext(AuthContext);

	const saveFile = (e) => {
		setFile(e.target.files[0]);
		setFileName(e.target.files[0].name);
	};

	const uploadFile = async () => {
		const formData = new FormData();
		formData.append("formFile", file);
		formData.append("fileName", fileName);
		formData.append("roomId", localStorage.getItem("room"));

		try {
			const res = await axios.post("/api/file", formData);
			props.sendMessage({ type: "link", value: fileName, file: res.data });
			formData.append("formFile", "");
			formData.append("fileName", "");

			axios.post("api/chat_mensagens", {
				Texto: fileName,
				Anexo: res.data,
				Anexo_Nome: fileName,
				Id_Grupo: localStorage.getItem("room"),
				Id_Utilizador: localStorage.getItem("user"),
			});
		} catch (ex) {
			console.log(ex);
		}
	};

	const postMessage = () => {
		axios
			.post("api/chat_mensagens", {
				Texto: message.value,
				Anexo: "",
				Anexo_Nome: "",
				Id_Grupo: localStorage.getItem("room"),
				Id_Utilizador: localStorage.getItem("user"),
			})
			.then(function (response) {
				console.log(response.data);
			});
		props.sendMessage({ type: "text", value: message.value });
	};

	return (
		<div className='border-t-2 border-gray-400 px-4 pt-4 mb-16'>
			<form
				onSubmit={(e) => {
					e.preventDefault();
					setMessage({ type: "text", value: "" });
					postMessage();
				}}
			>
				<div className='relative flex'>
					<span className='absolute inset-y-0 flex items-center'>
						<button
							type='file'
							onChange={saveFile}
							className={
								"inline-flex items-center justify-center rounded-full h-12 w-12 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300"
							}
						>
							<FontAwesomeIcon icon={faPaperclip} />
						</button>
						<button
							type='button'
							onClick={uploadFile}
							className={
								"inline-flex items-center justify-center rounded-full h-12 w-12 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300"
							}
						>
							<FontAwesomeIcon icon={faUpload} />
						</button>
						<button
							disabled={!message.value}
							className={
								"inline-flex items-center justify-center rounded-full h-12 w-12 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300"
							}
						>
							<FontAwesomeIcon icon={faPaperPlane} />
						</button>
					</span>

					<input
						className={
							"focus:ring-red-500 focus:border-red-500 w-full focus:placeholder-gray-400 text-gray-600 placeholder-gray-300 pl-36 bg-gray-100 rounded-full py-3 border-gray-200"
						}
						type='user'
						placeholder=' Mensagem'
						onChange={(e) =>
							setMessage({ type: "text", value: e.target.value })
						}
						value={message.value}
					/>

					<input type='file' onChange={saveFile} />
					<input type='button' value='upload' onClick={uploadFile} />
				</div>
			</form>
		</div>
	);
}
