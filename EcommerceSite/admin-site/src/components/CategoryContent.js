import React from "react";
import ApiCaller from "../helpers/ApiCaller";
import DataTable from "./DataTable";
export default class CategoryContent extends React.Component {
	state = {
		categories: [],
	};

	componentDidMount() {
		const caller = new ApiCaller();
		caller
			.getAllCategories()
			.then((data) => this.setState({ categories: data }));
	}

	render() {
		return <DataTable datasource={this.state.categories} />;
	}
}
