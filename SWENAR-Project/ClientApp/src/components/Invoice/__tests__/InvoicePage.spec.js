import React from "react";
import InvoicePage from "../InvoicePage";
import { mount } from "enzyme";

describe("Invoice Page tests", () => {
  it("shows customer forms with things rendered", () => {
    const props = { invoice: [] };
    const wrapper = mount(<InvoicePage {...props}></InvoicePage>);
    expect(wrapper).toMatchSnapshot();
    wrapper.unmount();
  });
});
