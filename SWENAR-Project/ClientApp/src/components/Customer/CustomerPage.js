import React, { Component } from "react";
import { withRouter, Redirect } from "react-router";
import { URL } from "./CustomerConstants";
import "ag-grid-community/dist/styles/ag-grid.css";
import "ag-grid-community/dist/styles/ag-theme-balham.css";
import CustomerFormEdit from "./CustomerFormEdit";
import { InvoicePage } from "../Invoice/InvoicePage";

//default style of the page
const style = {
  display: "flex",
  flexDirection: "column",
  fontSize: "12px",
};

class CustomerPage extends Component {
  static displayName = CustomerPage.name;
  //the default state of the app being no customer name, number and redirect being false
  constructor(props) {
    super(props);
    this.state = {
      Name: "",
      Number: "",
      redirect: false,
    };
  }

  //if a page refresh or load takes place fetch a customer by id
  componentDidMount = () => {
    const id = this.props.match.params.id;
    this.fetchCustomer(id);
  };

  //if a customer property updateds update the field
  componentDidUpdate = (prevProps, prevState) => {
    if (
      prevState.Name !== this.state.Name ||
      prevState.Number !== this.state.Number
    ) {
      const id = this.props.match.params.id;
      this.fetchCustomer(id);
    }
  };

  setCustomerData(data) {
    this.setState({
      Name: data.name,
      Number: data.number,
    });
  }

  //network fetch to the single customer end point
  fetchCustomer = (id) => {
    fetch(`${URL}/${id}`)
      .then((res) => res.json())
      .then((data) => {
        this.setCustomerData(data);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  //updates the state of name and number
  handleNameNumberchange = (Name, Number) => {
    this.setState({
      Name,
      Number,
    });
  };

  //makes a network call to delete customer information
  handleDelete = () => {
    let curr = this;
    const id = this.props.match.params.id;
    const axios = require("axios");
    axios
      .delete(`/api/customer/${id}`)
      .then(function (response) {
        curr.setState({ redirect: true });
      })
      .catch(function (error) {});
  };

  render = () => {
    //if you delete a customer send you back to the customers page
    if (this.state.redirect) {
      return <Redirect push to='/customerspage' />;
    }
    const { id } = this.props.match.params;

    // render out the customer page
    return (
      <div>
        <hr />
        <h1>Customer Page</h1>
        <div>
          <p>Customer Name: {this.state.Name}</p>
          <p>Customer Number: {this.state.Number}</p>

          <div style={style}>
            <CustomerFormEdit
              id={id}
              handleNameNumberchange={this.handleNameNumberchange}
            />
            <button onClick={this.handleDelete}>Delete</button>
          </div>
          <InvoicePage id={id} />
        </div>
      </div>
    );
  };
}

export default withRouter(CustomerPage);
