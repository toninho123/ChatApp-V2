import { createContext, useState } from "react";
import React from "react";

export const AuthContext = createContext({});

const AuthProvider = (props) => {
	const [user, setUser] = useState({ id: 0 });
	const [userName, setUserName] = useState("");

	return (
		<AuthContext.Provider value={{ user, setUser, userName, setUserName }}>
			{props.children}
		</AuthContext.Provider>
	);
};

export default AuthProvider;
