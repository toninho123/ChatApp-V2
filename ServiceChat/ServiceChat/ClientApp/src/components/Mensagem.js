import React from "react";

export default function Mensagem({ classe, corTexto, utilizador, texto }) {
	return (
		<div
			id='message'
			className='flex flex-col space-y-4 p-3 overflow-y-auto scrollbar-thumb-blue scrollbar-thum-rounded scrollbar-track-blue-lighter scrollbar-w-2 scrolling-touch'
		>
			<div className='chat-message'>
				<div className={"flex items-end " + classe}>
					<div className='flex flex-col space-y-2 text-sm max-w-xs mx-2 order-2 items-start'>
						<div>
							<span
								className={
									"px-4 py-2 rounded-lg inline-block rounded-bl-none " +
									corTexto +
									" text-gray-800"
								}
							>
								<h1>{utilizador}</h1>
								{texto}
							</span>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
