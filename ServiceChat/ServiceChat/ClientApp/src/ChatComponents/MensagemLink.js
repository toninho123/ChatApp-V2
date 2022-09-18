import React from "react";

export default function MensagemLink({
	classe,
	utilizador,
	texto,
	file,
	corTexto,
}) {
	return (
		<div
			id='message'
			className='flex flex-col space-y-4 p-3 overflow-y-auto scrollbar-thumb-blue scrollbar-thumb-rounded scrollbar-track-blue-lighter scrollbar-w-2 scrolling-touch'
		>
			<div className={"chat-message " + classe}>
				<div className='flex items-end'>
					<div className='flex flex-col space-y-2 text-xs max-w-xs mx-2 order-2 items-start'>
						<div>
							<span
								className={
									"px-4 py-2 rounded-lg inline-block rounded-bl-none " +
									corTexto +
									" text-gray-600"
								}
							>
								<a
									href={"/" + file}
									target='_blank'
									download={texto}
									rel='noopener noreferrer'
								>
									{utilizador + " - " + texto}
								</a>
							</span>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
