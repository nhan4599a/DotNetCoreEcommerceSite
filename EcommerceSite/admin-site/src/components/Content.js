import React from "react";
import { Route } from "react-router-dom";
import CategoryContent from "./CategoryContent";
import ProductContent from "./ProductContent";
import UserContent from "./UserContent";

export default class Content extends React.Component {
	render() {
		return (
			<>
				<div className="clearfix"></div>
				<div className="content-wrapper">
					<div className="container-fluid">
						<div className="row mt-3">
							<div className="col-lg-8 offset-lg-2">
								<Route
									path="/categories"
									exact
									component={CategoryContent}
								></Route>
								<Route
									path="/products"
									exact
									component={ProductContent}
								></Route>
								<Route
									path="/users"
									exact
									component={UserContent}
								></Route>
							</div>
						</div>
					</div>
				</div>
			</>
		);
	}
}
