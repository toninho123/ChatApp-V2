import { createContext, useState } from "react";
import React from "react";

export const AdminContext = createContext({});

const AdminProvider = (props) => {
	const [admin, setAdmin] = useState({ adm: false });

	return (
		<AdminContext.Provider value={{ admin, setAdmin }}>
			{props.children}
		</AdminContext.Provider>
	);
};

export default AdminProvider;
