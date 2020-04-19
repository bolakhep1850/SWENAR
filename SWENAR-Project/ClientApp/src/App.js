import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { FetchData } from "./components/FetchData";
import { Counter } from "./components/Counter";
import { CustomersPage } from "./components/Customer/CustomersPage";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";
import { CustomerPage } from "./components/Customer/CustomerPage";
import { ExcelLoad } from "./components/Invoice/ExcelLoad";

import "./bootstrap.min.css";
import "./custom.css";

class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/customerspage' component={CustomersPage} />
        <Route path='/excelload' component={ExcelLoad} />
        <Route path='/customerpage/:id' component={CustomerPage} />
        <AuthorizeRoute path='/fetch-data' component={FetchData} />
        <Route
          path={ApplicationPaths.ApiAuthorizationPrefix}
          component={ApiAuthorizationRoutes}
        />
      </Layout>
    );
  }
}

export default App;
