import React from "react";
import CustomerPage from "../CustomerPage";
import { shallow } from "enzyme";
import axios from "axios";

describe("CustomerForm tests", () => {
  it("shows customer forms with things rendered", () => {
    const props = {
      Name: "",
      number: "",
      redirect: false,
    };

    const wrapper = shallow(<CustomerPage {...props} />);
    expect(wrapper.instance()).not.toEqual(null);
  });
});
