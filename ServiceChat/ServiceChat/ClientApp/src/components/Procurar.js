import React from "react";

export default function Procurar(props) {
	const handleClick = (e) => {
		e.preventDefault();
		props.setSearch(e.target.value);
	};

	return (
		<div className='mb-6'>
			<div className='relative'>
				<input
					placeholder='Procurar...'
					name='search'
					onChange={(e) => handleClick(e)}
					className='focus:ring-red-500 focus:border-red-500 block w-full pl-10 sm:text-sm border-x-gray-100 rounded-full p-2 border'
				/>
			</div>
		</div>
	);
}
