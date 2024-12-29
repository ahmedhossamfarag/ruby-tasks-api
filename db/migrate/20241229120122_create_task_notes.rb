class CreateTaskNotes < ActiveRecord::Migration[8.0]
  def change
    create_table :task_notes do |t|
      t.integer :user_id
      t.string :title
      t.string :description
      t.integer :priority
      t.datetime :duedate
    end
    add_foreign_key :task_notes, :users, column: :user_id
  end
end
