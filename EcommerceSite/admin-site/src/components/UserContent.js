import React from "react";
import DataTable from "./DataTable";
import ApiCaller from "../helpers/ApiCaller";

export default class UserContent extends React.Component {
	state = {
		users: [],
	};

	componentDidMount() {
		const caller = new ApiCaller();
		caller.getAllUsers().then((data) => this.setState({ users: data }));
	}

	render() {
		return <DataTable datasource={this.state.users} title="Users" />;
	}
}
