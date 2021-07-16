import LeftNavBar from "./components/LeftNavBar";
import TopNavBar from "./components/TopNavBar";
import { BrowserRouter as Router } from "react-router-dom";
import logo from "./logo.svg";
import Content from "./components/Content";
import AuthService from "./Helper/AuthHelper";

function App() {
	const authService = new AuthService();

	return (
		<Router>
			<LeftNavBar></LeftNavBar>
			<TopNavBar isLoggedIn={authService.isLoggedIn()}></TopNavBar>
			<Content></Content>
		</Router>
	);
}

export default App;
