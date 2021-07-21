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
						<Link to="/categories">
							<i className="zmdi zmdi-view-dashboard"></i>{" "}
							<span>Categories</span>
						</Link>
					</li>
					<li>
						<Link to="/products">
							<i className="zmdi zmdi-view-dashboard"></i>{" "}
							<span>Products</span>
						</Link>
					</li>
					<li>
						<Link to="/users">
							<i className="zmdi zmdi-view-dashboard"></i>{" "}
							<span>Users</span>
						</Link>
					</li>
				</ul>
			</div>
		);
	}
}
