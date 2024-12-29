class TaskNote < ApplicationRecord
  belongs_to :user
  validates :title, presence: true, length: { within: 3..20 }
  validates :description, length: { maximum: 255 }
  validates :user_id, presence: true, numericality: { only_integer: true }
  validates :priority, presence: true
  validates :duedate, presence: true, comparison: { greater_than_or_equal_to: DateTime.now }


  enum :priority, [ :low, :medium, :high ], validate: true
end
