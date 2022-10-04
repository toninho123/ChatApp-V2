import { createContext, useState } from "react";
import React from "react";

export const RoomContext = createContext({});

const RoomProvider = (props) => {
	const [room, setRoom] = useState("");
	const [roomName, setRoomName] = useState({ roomName: "" });
	const [data, setData] = useState({ data: [] });

	return (
		<RoomContext.Provider
			value={{ room, setRoom, roomName, setRoomName, data, setData }}
		>
			{props.children}
		</RoomContext.Provider>
	);
};

export default RoomProvider;
