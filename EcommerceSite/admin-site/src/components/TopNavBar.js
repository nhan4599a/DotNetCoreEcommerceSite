import React from "react";

export default class TopNavBar extends React.Component {
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

					<a href="/">{this.props.isLoggedIn ? "Hello" : "Login"}</a>
				</nav>
			</header>
		);
	}
}
