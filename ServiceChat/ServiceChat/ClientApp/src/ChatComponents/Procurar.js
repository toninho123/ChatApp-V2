import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";

export default function Procurar(props) {
	const handleClick = (e) => {
		e.preventDefault();
		props.setSearch(e.target.value);
	};

	return (
		<div className='mb-6'>
			<div className='relative'>
				<div className='relative'>
					<span className='absolute inset-y-0 left-0 pl-3 flex items-center'>
						<FontAwesomeIcon icon={faMagnifyingGlass} />
					</span>

					<input
						type='search'
						placeholder='Procurar...'
						onChange={(e) => handleClick(e)}
						className='focus:ring-red-500 focus:border-red-500 block w-full pl-10 sm:text-sm border-x-gray-100 rounded-full p-2 border'
					/>
				</div>
			</div>
		</div>
	);
}
