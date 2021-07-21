import React from "react";
import DataTable from "./DataTable";
import ApiCaller from "../helpers/ApiCaller";

export default class ProductContent extends React.Component {
	state = {
		products: [],
	};

	componentDidMount() {
		const caller = new ApiCaller();
		caller
			.getAllProducts()
			.then((data) => this.setState({ products: data }));
	}

	render() {
		return <DataTable datasource={this.state.products} />;
	}
}
