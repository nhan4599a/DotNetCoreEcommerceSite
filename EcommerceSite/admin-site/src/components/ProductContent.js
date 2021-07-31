import React from "react";
import DataTable from "./DataTable";
import ApiCaller from "../helpers/ApiCaller";

export default class ProductContent extends React.Component {
	caller;
	constructor() {
		super();
		this.caller = new ApiCaller();
	}
	state = {
		products: [],
		categories: [],
		product_name: "",
		product_description: "",
		product_price: 0,
		product_category: "0",
		product_image: "",
		image: "",
	};

	handleAddProduct(e) {
		e.preventDefault();
		var formData = new FormData();
		formData.append("name", e.target.product_name.value);
		formData.append("description", e.target.product_description.value);
		formData.append("price", e.target.product_price.value);
		formData.append("image", this.state.image);
		this.caller
			.addProduct(formData, e.target.product_category.value)
			.then((data) => {
				this.setState({
					products: [...this.state.products, data.data.data],
					product_name: "",
					product_description: "",
					product_price: 0,
					product_image: "",
					image: "",
				});
			});
	}

	handleValueChanged(e) {
		var newObject = {};
		newObject[e.target.name] = e.target.value;
		this.setState(newObject);
	}

	handleFileChoosed(e) {
		var file = e.target.files[0];
		if (file) {
			var link = URL.createObjectURL(file);
			this.setState({
				product_image: link,
				image: file,
			});
		}
	}

	revokeObject() {
		URL.revokeObjectURL(this.state.product_image);
	}

	componentDidMount() {
		this.caller
			.getAllProducts()
			.then((data) => this.setState({ products: data.data.data }));
		this.caller
			.getAllCategories()
			.then((data) => this.setState({ categories: data.data.data }));
	}

	render() {
		return (
			<>
				<DataTable datasource={this.state.products} title="Product" />
				<div className="card">
					<div className="card-body">
						<div className="card-title">Add product</div>
						<hr></hr>
						<form
							onSubmit={(e) => this.handleAddProduct(e)}
							encType="multipart/form-data"
						>
							<div className="form-group">
								<label className="input-6">Name</label>
								<input
									type="text"
									className="form-control form-control-rounded"
									placeholder="Enter product name"
									name="product_name"
									value={this.state.product_name}
									onChange={(e) => this.handleValueChanged(e)}
								/>
							</div>
							<div className="form-group">
								<label className="input-6">Description</label>
								<input
									type="text"
									className="form-control form-control-rounded"
									placeholder="Enter product description"
									name="product_description"
									value={this.state.product_description}
									onChange={(e) => this.handleValueChanged(e)}
								/>
							</div>
							<div className="form-group">
								<label className="input-6">Price</label>
								<input
									type="number"
									className="form-control form-control-rounded"
									placeholder="Enter product price"
									name="product_price"
									min="0"
									value={this.state.product_price}
									onChange={(e) => this.handleValueChanged(e)}
								/>
							</div>
							<div className="form-group">
								<label className="input-6">Category</label>
								<select
									name="product_category"
									className="form-control form-control-rounded form-select"
								>
									<option
										value={this.state.product_category}
										onChange={(e) =>
											this.handleValueChanged(e)
										}
									>
										Select category
									</option>
									{this.state.categories[0] &&
										this.state.categories.map((item) => (
											<option value={item.id}>
												{item.name}
											</option>
										))}
								</select>
							</div>
							<div className="form-group">
								<label
									htmlFor="product_image"
									className="form-label"
								>
									image
								</label>
								<input
									type="file"
									id="product_image"
									name="product_image"
									className="form-control"
									onChange={(e) => this.handleFileChoosed(e)}
								/>
								<img
									src={this.state.product_image}
									alt="product"
									onLoad={() =>
										URL.revokeObjectURL(
											this.state.product_image
										)
									}
									width="200px"
									height="200px"
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
