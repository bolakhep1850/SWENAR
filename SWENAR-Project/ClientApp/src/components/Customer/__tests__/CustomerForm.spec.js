import React from "react";
import CustomerForm from "../CustomerForm";
import { shallow } from "enzyme";
import axios from "axios";

describe("CustomerForm tests", () => {
  it("shows customer forms with things rendered", () => {
    const props = {
      Name: "",
      number: "",
    };
    const wrapper = shallow(<CustomerForm {...props}></CustomerForm>);
    expect(wrapper).toMatchSnapshot();
  });

  it("Should call setState on input", () => {
    const mockChange = {
      target: {
        name: "input",
        value: "test 123",
      },
    };

    const expected = {
      Name: "",
      number: "",
      input: "test 123",
      error: false,
    };

    const props = {
      Name: "",
      number: "",
    };

    const wrapper = shallow(<CustomerForm {...props} />);

    wrapper.instance().onChange(mockChange);

    expect(wrapper.state()).toEqual(expected);
  });

  it("Should call submitToPost", () => {
    const props = {
      Name: "",
      number: "",
      handleNumName: jest.spyOn(axios, "post"),
    };

    const mockChange = {
      target: {
        name: "input",
        value: "test 123",
      },
    };

    const wrapper = shallow(<CustomerForm {...props} />);

    wrapper.instance().onChange(mockChange);
    wrapper.instance().submitToPost();

    wrapper.find("button").simulate("click");
    expect(props.handleNumName).toHaveBeenCalled();
    wrapper.unmount();
  });
});
