require 'rails_helper'

RSpec.describe "Users", type: :request do
  describe "POST /create" do
    it "returns http success" do
      post "/users/create", params: { user: { username: "username1", password: "password1" } }
      expect(response).to have_http_status(:success)
      post "/users/create", params: { user: { username: "username" } }
      expect(response).to have_http_status(:bad_request)
    end
  end

  describe "POST /login" do
    it "returns http success" do
      User.create(username: "username", password: "password")
      post "/users/login", params: { user: { username: "username", password: "password" } }
      expect(response).to have_http_status(:ok), response.body
      post "/users/login", params: { user: { username: "username" } }
      expect(response).to have_http_status(:bad_request)
    end
  end

  describe "GET /logout" do
    it "returns http ok" do
      get "/users/logout"
      expect(response).to have_http_status(:unauthorized)
      User.create(username: "username", password: "password")
      post "/users/login", params: { user: { username: "username", password: "password" } }
      get "/users/logout"
      expect(response).to have_http_status(:ok)
    end
  end

end
