import React, { Component } from "react";

class CustomerFormEdit extends Component {
  //default state of app is empty name and number
  constructor(props) {
    super(props);

    this.state = {
      Name: "",
      number: "",
      error: undefined,
    };
  }

  //onchange handler for text input
  onChange = (e) => {
    this.setState({
      [e.target.name]: e.target.value,
    });
  };

  //call to back end to edit an existing name
  submitToPUT = () => {
    let { id } = this.props;
    id = parseInt(id);
    const axios = require("axios");
    const { Name, number } = this.state;
    this.props.handleNameNumberchange(Name, number);

    axios
      .put(`/api/customer/${id}`, {
        id,
        Name,
        Number: number,
      })
      .then(function (response) {
        console.log(response);
      })
      .catch(function (error) {
        this.setState({ error: error });
      });
  };

  render = () => {
    return (
      <div>
        <form>
          <label htmlFor='Name'>Enter new name: </label>
          <input id='Name' name='Name' type='text' onChange={this.onChange} />

          <label htmlFor='Number'>Enter new number:</label>
          <input
            id='number'
            name='number'
            type='text'
            onChange={this.onChange}
          />
        </form>
        <div>
          <button onClick={this.submitToPUT}>Save</button>
        </div>
      </div>
    );
  };
}

export default CustomerFormEdit;
