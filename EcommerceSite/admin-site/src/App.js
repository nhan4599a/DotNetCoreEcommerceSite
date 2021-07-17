import LeftNavBar from "./components/LeftNavBar";
import TopNavBar from "./components/TopNavBar";
import { BrowserRouter as Router, Route } from "react-router-dom";
import Content from "./components/Content";
import Login from "./components/Login";
import PostLogin from "./components/PostLogin";

function App() {
	return (
		<Router>
			<LeftNavBar></LeftNavBar>
			<TopNavBar></TopNavBar>
			<Content></Content>
			<Route path="/login" exact component={Login}></Route>
			<Route path="/post-login" exact component={PostLogin}></Route>
		</Router>
	);
}

export default App;
