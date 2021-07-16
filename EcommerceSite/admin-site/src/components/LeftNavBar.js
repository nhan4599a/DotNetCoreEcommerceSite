import React from "react";
import { Link } from "react-router-dom";

export default class LeftNavBar extends React.Component {
	render() {
		return (
			<div id="sidebar-wrapper">
				<div className="brand-logo">
					<a href="index.html">
						<img
							src="assets/images/logo-icon.png"
							className="logo-icon"
							alt="logo icon"
						/>
						<h5 className="logo-text">Dashtreme Admin</h5>
					</a>
				</div>
				<ul className="sidebar-menu do-nicescrol">
					<li className="sidebar-header">MAIN NAVIGATION</li>
					<li>
						<Link to="/hello">
							<i className="zmdi zmdi-view-dashboard"></i>{" "}
							<span>Dashboard</span>
						</Link>
					</li>

					<li>
						<Link to="/goodbye">
							<i className="zmdi zmdi-invert-colors"></i>{" "}
							<span>UI Icons</span>
						</Link>
					</li>

					<li>
						<a href="forms.html">
							<i className="zmdi zmdi-format-list-bulleted"></i>{" "}
							<span>Forms</span>
						</a>
					</li>

					<li>
						<a href="tables.html">
							<i className="zmdi zmdi-grid"></i>{" "}
							<span>Tables</span>
						</a>
					</li>

					<li>
						<a href="calendar.html">
							<i className="zmdi zmdi-calendar-check"></i>{" "}
							<span>Calendar</span>
							<small className="badge float-right badge-light">
								New
							</small>
						</a>
					</li>

					<li>
						<a href="profile.html">
							<i className="zmdi zmdi-face"></i>{" "}
							<span>Profile</span>
						</a>
					</li>

					<li>
						<a href="login.html" target="_blank">
							<i className="zmdi zmdi-lock"></i>{" "}
							<span>Login</span>
						</a>
					</li>

					<li>
						<a href="register.html" target="_blank">
							<i className="zmdi zmdi-account-circle"></i>{" "}
							<span>Registration</span>
						</a>
					</li>
				</ul>
			</div>
		);
	}
}
