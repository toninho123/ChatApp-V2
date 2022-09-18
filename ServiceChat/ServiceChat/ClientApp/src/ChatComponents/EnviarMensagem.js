import React, { useState, useContext } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPaperclip } from "@fortawesome/free-solid-svg-icons";
import { faUpload } from "@fortawesome/free-solid-svg-icons";
import { faPaperPlane } from "@fortawesome/free-solid-svg-icons";
import axios from "axios";
import { AuthContext } from "../provider/Auth";
import { RoomContext } from "../provider/Room";

export default function EnviarMensagem(props) {
	const buttonStyle =
		"inline-flex items-center justify-center rounded-full h-12 w-12 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300";
	const inputStyle =
		"focus:ring-red-500 focus:border-red-500 w-full focus:placeholder-gray-400 text-gray-600 placeholder-gray-300 pl-36 bg-gray-100 rounded-full py-3 border-gray-200";

	const [message, setMessage] = useState({});
	const [file, setFile] = useState();
	const [fileName, setFileName] = useState();
	const room = useContext(RoomContext);
	const user = useContext(AuthContext);

	const saveFile = (e) => {
		console.log(e.target.files[0]);
		setFile(e.target.files[0]);
		setFileName(e.target.files[0].name);
	};

	const uploadFile = async () => {
		const formData = new FormData();
		formData.append("formFile", file);
		formData.append("fileName", fileName);
		formData.append("roomId", room.room);

		try {
			const res = await axios.post("/api/file", formData);
			console.log("RESPOND: ", res.data);
			props.sendMessage({ type: "link", value: fileName, file: res.data });
			formData.append("formFile", "");
			formData.append("fileName", "");

			axios
				.post("api/mensagem", {
					Texto: fileName,
					Ficheiro: res.data,
					Id_Sala: room.room,
					Id_Utilizador: user.user,
				})
				.then(function (response) {
					console.log(response.data);
				});
		} catch (ex) {
			console.log(ex);
		}
	};

	const postMessage = () => {
		axios
			.post("api/mensagem", {
				Texto: message.value,
				Ficheiro: "",
				Id_Sala: room.room,
				Id_Utilizador: user.user,
			})
			.then(function (response) {
				console.log(response.data);
			});
		props.sendMessage({ type: "text", value: message.value });
	};

	return (
		<div className='border-t-2 border-gray-200 px-4 pt-4 mb-16'>
			<form
				onSubmit={(e) => {
					e.preventDefault();
					setMessage({ type: "text", value: "" });
					postMessage();
				}}
			>
				<div className='relative flex'>
					<span className='absolute inset-y-0 flex items-center'>
						<button type='file' onClick={saveFile} className={buttonStyle}>
							<FontAwesomeIcon icon={faPaperclip} />
						</button>
						<button className={buttonStyle} onClick={uploadFile}>
							<FontAwesomeIcon icon={faUpload} />
						</button>
						<button className={buttonStyle}>
							<FontAwesomeIcon icon={faPaperPlane} />
						</button>
					</span>
					<input
						className={inputStyle}
						type='user'
						placeholder=' Mensagem'
						onChange={(e) =>
							setMessage({ type: "text", value: e.target.value })
						}
						value={message.value}
					/>
				</div>
			</form>
		</div>
	);
}
