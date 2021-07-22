import React from "react";
import ApiCaller from "../helpers/ApiCaller";
import DataTable from "./DataTable";
export default class CategoryContent extends React.Component {
	caller;

	constructor() {
		super();
		this.caller = new ApiCaller();
	}

	state = {
		categories: [],
		name: "",
	};

	handleAddCategory(e) {
		e.preventDefault();
		var name = e.target.category_name.value;
		this.caller.addCategory(name).then((addedCategory) => {
			this.setState({
				categories: [...this.state.categories, addedCategory.data.data],
			});
		});
	}

	handleTextChanged(e) {
		this.setState({
			name: e.target.value,
		});
	}

	componentDidMount() {
		this.caller.getAllCategories().then((data) => {
			this.setState({
				categories: data.data.data,
			});
		});
	}

	render() {
		return (
			<>
				<DataTable
					datasource={this.state.categories}
					title="Categories"
				/>
				<div className="card">
					<div className="card-body">
						<div className="card-title">Add category</div>
						<hr></hr>
						<form onSubmit={(e) => this.handleAddCategory(e)}>
							<div className="form-group">
								<label className="input-6">Name</label>
								<input
									type="text"
									className="form-control form-control-rounded"
									placeholder="Enter category name"
									name="category_name"
									value={this.state.name}
									onChange={(e) => this.handleTextChanged(e)}
								/>
							</div>
							<div className="form-group">
								<button
									type="submit"
									className="btn btn-light btn-round px-5"
								>
									Add
								</button>
							</div>
						</form>
					</div>
				</div>
			</>
		);
	}
}
