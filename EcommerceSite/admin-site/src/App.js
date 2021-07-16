import LeftNavBar from "./components/LeftNavBar";
import TopNavBar from "./components/TopNavBar";
import { BrowserRouter as Router } from "react-router-dom";
import logo from "./logo.svg";
import Content from "./components/Content";

function App() {
	var accessToken = localStorage.getItem("access_token");
	return (
		<>
			{accessToken}
			<Router>
				<LeftNavBar></LeftNavBar>
				<TopNavBar></TopNavBar>
				<Content></Content>
			</Router>
		</>
	);
}

export default App;
