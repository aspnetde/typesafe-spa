import React from "react";
import { Link } from "react-router-dom";

export default function Home() {
  return <Link to="/login">Hello, stranger! You're not signed in.</Link>;
}
