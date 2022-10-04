import React from "react";

export default function Mensagem({
	classe,
	utilizador,
	texto,
	file,
	corTexto,
	addUtilizador,
}) {
	return (
		<div
			id='message'
			className='flex flex-col space-y-4 p-3 overflow-y-auto scrollbar-thumb-blue scrollbar-thumb-rounded scrollbar-track-blue-lighter scrollbar-w-2 scrolling-touch'
		>
			<div className='chat-message'>
				<div className={"flex items-end " + classe}>
					<div className='flex flex-col space-y-2 text-sm max-w-xs mx-2 order-2 items-start'>
						<div>
							<h1>{utilizador}</h1>
							<a
								className={
									"px-4 py-2 rounded-lg inline-block rounded-bl-none " +
									corTexto +
									" text-gray-800"
								}
								href={"/" + file}
								target='_blank'
								download={texto}
								rel='noopener noreferrer'
							>
								{texto}
							</a>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
