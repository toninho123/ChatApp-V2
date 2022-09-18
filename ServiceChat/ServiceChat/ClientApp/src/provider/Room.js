import { createContext, useState } from "react";
import React from "react";

export const RoomContext = createContext({});

const RoomProvider = (props) => {
	const [room, setRoom] = useState("");
	const [roomName, setRoomName] = useState({ roomName: "" });

	return (
		<RoomContext.Provider value={{ room, setRoom, roomName, setRoomName }}>
			{props.children}
		</RoomContext.Provider>
	);
};

export default RoomProvider;
