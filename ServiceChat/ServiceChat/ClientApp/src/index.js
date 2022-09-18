import "bootstrap/dist/css/bootstrap.css";
import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import AuthProvider from "./provider/Auth";
import AdminProvider from "./provider/Admin";
import RoomProvider from ".//provider/Room";

import App from "./App";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");

ReactDOM.render(
	<BrowserRouter basename={baseUrl}>
		<AuthProvider>
			<AdminProvider>
				<RoomProvider>
					<App />
				</RoomProvider>
			</AdminProvider>
		</AuthProvider>
	</BrowserRouter>,
	rootElement
);
