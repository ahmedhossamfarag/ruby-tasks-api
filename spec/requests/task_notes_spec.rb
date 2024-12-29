require 'rails_helper'

RSpec.describe "TaskNotes", type: :request do
  before(:all) do
    @user = User.create(username: 'username2', password: 'password2')
    @user = User.find_by(username: 'username2')
    post '/users/login', params: { user: { username: 'username2', password: 'password2' } }
    expect(response).to have_http_status(:ok)
  end

  describe "GET /show" do
    it "returns http success" do
      get "/task_notes/show"
      expect(response).to have_http_status(:success)
    end
  end

  describe "POST /create" do
    it "returns http success" do
      post "/task_notes/create", params: { task_note: { title: "task", priority: :low, duedate: "1-1-2028" } }
      expect(response).to have_http_status(:success), response.body
      expect(TaskNote.find_by(user_id: @user.id, title: "task")).not_to eq(nil)
    end
  end

  describe "PATCH /update" do
    it "returns http success" do
      task_note = TaskNote.create(user_id: @user.id, title: "task", priority: :low, duedate: "1-1-2028")
      patch "/task_notes/update", params: { id: task_note.id, task_note: { title: "task#", priority: :low, duedate: "1-1-2028" } }
      expect(response).to have_http_status(:success)
      expect(TaskNote.find_by(id: task_note.id, title: "task#")).not_to eq(nil)
    end
  end

  describe "DELETE /delete" do
    it "returns http success" do
      task_note = TaskNote.create(user_id: @user.id, title: "task", priority: :low, duedate: "1-1-2028")
      delete "/task_notes/delete", params: { id: task_note.id }
      expect(response).to have_http_status(:success)
      expect(TaskNote.find_by(id: task_note.id)).to eq(nil)
    end
  end

end
