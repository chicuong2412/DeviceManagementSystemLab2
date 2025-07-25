// MongoDB Aggregation Pipeline for Conditional LocalDateTime Calculation

{
  $addFields: {
    LocalDateTime: {
      $cond: {
        if: { $gt: ["$Data.DateTime", "$ModifiedDate"] },
        then: {
          $add: [
            "$ModifiedDate",
            7 * 60 * 60 * 1000  // 7 hours in milliseconds
          ]
        },
        else: {
          $add: [
            "$Data.DateTime",
            7 * 60 * 60 * 1000  // 7 hours in milliseconds
          ]
        }
      }
    }
  }
}

// Alternative syntax using $switch (if you have multiple conditions)
{
  $addFields: {
    LocalDateTime: {
      $switch: {
        branches: [
          {
            case: { $gt: ["$Data.DateTime", "$ModifiedDate"] },
            then: {
              $add: [
                "$ModifiedDate",
                7 * 60 * 60 * 1000
              ]
            }
          }
        ],
        default: {
          $add: [
            "$Data.DateTime",
            7 * 60 * 60 * 1000
          ]
        }
      }
    }
  }
}

// Complete aggregation pipeline example
db.collection.aggregate([
  {
    $addFields: {
      LocalDateTime: {
        $cond: {
          if: { $gt: ["$Data.DateTime", "$ModifiedDate"] },
          then: {
            $add: [
              "$ModifiedDate",
              7 * 60 * 60 * 1000
            ]
          },
          else: {
            $add: [
              "$Data.DateTime", 
              7 * 60 * 60 * 1000
            ]
          }
        }
      }
    }
  }
  // Add other pipeline stages as needed
])

// If working with Date objects instead of timestamps:
{
  $addFields: {
    LocalDateTime: {
      $cond: {
        if: { $gt: ["$Data.DateTime", "$ModifiedDate"] },
        then: {
          $dateAdd: {
            startDate: "$ModifiedDate",
            unit: "hour",
            amount: 7
          }
        },
        else: {
          $dateAdd: {
            startDate: "$Data.DateTime",
            unit: "hour", 
            amount: 7
          }
        }
      }
    }
  }
} 