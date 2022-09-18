import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAlignJustify } from "@fortawesome/free-solid-svg-icons";

export default function Navbar(props) {
	return (
		<nav className='flex-shrink-0 bg-red-600'>
			<div className='max-w-7xl mx-auto px-2 sm:px-4 lg:px-8'>
				<div className='relative flex items-center justify-between h-16'>
					<div></div>
					<div className='flex lg:hidden'>
						<button className='bg-red-600 inline-flex items-center justify-center p-2 rounded-md text-white hover:text-white hover:bg-red-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-red-600 focus:ring-white '>
							<FontAwesomeIcon
								icon={faAlignJustify}
								className='hover:text-black'
								size='lg'
							/>
						</button>
					</div>
					<div className='hidden lg:block lg:w-80'>
						<div className='flex items-center justify-end'>
							<div className='flex'>
								<a
									href='#'
									className='px-3 py-2 rounded-md text-sm font-medium text-white hover:text-white'
								>
									Portugues
								</a>
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
	);
}
