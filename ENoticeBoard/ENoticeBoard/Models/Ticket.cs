using System;

namespace ENoticeBoard.Models
{
    public class Ticket
    {

        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public int? assigned_to { get; set; }
            public object billing_rate { get; set; }
            public object c_location { get; set; }
            public string c_status { get; set; }
            public string category { get; set; }
            public DateTime? closed_at { get; set; }
            public DateTime created_at { get; set; }
            public int created_by { get; set; }
            public object due_at { get; set; }
            public string email_message_id { get; set; }
            public int error_alert_count { get; set; }
            public object external_id { get; set; }
            public int? first_response_secs { get; set; }
            public int id { get; set; }
            public object master_ticket_id { get; set; }
            public bool? muted { get; set; }
            public object parent_id { get; set; }
            public int priority { get; set; }
            public object remote_id { get; set; }
            public bool? reopened { get; set; }
            public object reported_by_id { get; set; }
            public object requires_purchase { get; set; }
            public object sharer_id { get; set; }
            public int site_id { get; set; }
            public string status { get; set; }
            public DateTime? status_updated_at { get; set; }
            public object synced_at { get; set; }
            public DateTime updated_at { get; set; }
            public DateTime? viewed_at { get; set; }
            public int warning_alert_count { get; set; }
            public Creator creator { get; set; }
            public Assignee assignee { get; set; }
            public Public_Comments[] public_comments { get; set; }
            public string ticket_url { get; set; }
            public string summary { get; set; }
            public string description { get; set; }
        }

        public class Creator
        {
            public string email { get; set; }
            public string first_name { get; set; }
            public int id { get; set; }
            public string last_name { get; set; }
        }

        public class Assignee
        {
            public string email { get; set; }
            public string first_name { get; set; }
            public int id { get; set; }
            public string last_name { get; set; }
        }

        public class Public_Comments
        {
            public string attachment_content_type { get; set; }
            public string attachment_location { get; set; }
            public string attachment_name { get; set; }
            public int? attachment_size { get; set; }
            public string body { get; set; }
            public object collaborator_id { get; set; }
            public string comment_type { get; set; }
            public DateTime created_at { get; set; }
            public int created_by { get; set; }
            public int id { get; set; }
            public bool is_inventory { get; set; }
            public object is_labor { get; set; }
            public bool is_public { get; set; }
            public bool is_purchase { get; set; }
            public object remote_id { get; set; }
            public int ticket_id { get; set; }
            public DateTime updated_at { get; set; }
            public Creator1 creator { get; set; }
        }

        public class Creator1
        {
            public string cell_phone { get; set; }
            public int? community_activity_count { get; set; }
            public DateTime? community_activity_seen_at { get; set; }
            public int? community_unread_message_count { get; set; }
            public int company_id { get; set; }
            public DateTime created_at { get; set; }
            public string department { get; set; }
            public DateTime? disabled { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public object home_page { get; set; }
            public int id { get; set; }
            public string last_name { get; set; }
            public string location { get; set; }
            public int notification_preferences { get; set; }
            public string office_phone { get; set; }
            public DateTime? registered { get; set; }
            public string role { get; set; }
            public string start_date { get; set; }
            public int? supervisor_id { get; set; }
            public string title { get; set; }
            public DateTime updated_at { get; set; }
        }

    }
}