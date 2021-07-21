import React from "react";
import { Link } from "react-router-dom";
import AuthHelper from "../helpers/AuthHelper";

export default class TopNavBar extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			userInfo: null,
		};
	}

	componentDidMount() {
		AuthHelper.getInstance()
			.getManager()
			.getUser()
			.then((user) => this.setState({ userInfo: user }));
	}

	render() {
		return (
			<header className="topbar-nav">
				<nav className="navbar navbar-expand fixed-top">
					<ul className="navbar-nav mr-auto align-items-center">
						<li className="nav-item">
							<a className="nav-link toggle-menu" href="/">
								<i className="icon-menu menu-icon"></i>
							</a>
						</li>
						<li className="nav-item">
							<form className="search-bar">
								<input
									type="text"
									className="form-control"
									placeholder="Enter keywords"
								/>
								<a href="/">
									<i className="icon-magnifier"></i>
								</a>
							</form>
						</li>
					</ul>
					<ul className="navbar-nav align-items-center right-nav-link">
						{this.state.userInfo ? (
							<Link to="/">Hello</Link>
						) : (
							<Link to="/login">Login</Link>
						)}
					</ul>
				</nav>
			</header>
		);
	}
}
