import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Home from "./components/Home";

function App() {
	return (
		<div>
			<div>
				<Routes>
					<Route path='/' element={<Login />} />
					<Route path='/hom' element={<Home />} />
				</Routes>
			</div>
		</div>
	);
}

export default App;
