require 'rails_helper'

RSpec.describe TaskNote, type: :model do

  before(:all) do
    User.create(username: "ahmed", password: "password")
  end

  it "test user_id validation" do
    task_note = TaskNote.new(user_id: 1, title: "task", priority: :low, duedate: DateTime.now)
    expect(task_note.valid?).to eq(true), task_note.errors.full_messages.to_s
    task_note.user_id = 123
    expect(task_note.valid?).to eq(false), task_note.errors.full_messages.to_s
  end

  it "test title validation" do
    task_note = TaskNote.new(user_id: 1, priority: :low, duedate: DateTime.now)
    expect(task_note.valid?).to eq(false), task_note.errors.full_messages.to_s
    task_note.title = "te"
    expect(task_note.valid?).to eq(false), task_note.errors.full_messages.to_s
  end

  it "test priority validation" do
    task_note = TaskNote.new(user_id: 1, title: "task", duedate: DateTime.now)
    expect(task_note.valid?).to eq(false), task_note.errors.full_messages.to_s
    task_note.priority = :high
    expect(task_note.valid?).to eq(true), task_note.errors.full_messages.to_s
    task_note.priority = 1
    expect(task_note.valid?).to eq(true), task_note.errors.full_messages.to_s
    task_note.priority = :other
    expect(task_note.valid?).to eq(false), task_note.errors.full_messages.to_s
  end

  it "test duedate validation" do
    task_note = TaskNote.new(user_id: 1, title: "task", priority: :low)
    expect(task_note.valid?).to eq(false), task_note.errors.full_messages.to_s
    task_note.duedate = "11-8-2016"
    expect(task_note.valid?).to eq(false), task_note.errors.full_messages.to_s
    task_note.duedate = "1-1-2028"
    expect(task_note.valid?).to eq(true), task_note.errors.full_messages.to_s
  end
end
