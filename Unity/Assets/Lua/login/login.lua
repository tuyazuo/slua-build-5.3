print("2222")

local cc = {}
function cc:init()
	print("3333")
	-- cjson.encode({})
	-- print(lpeg)
	local core = require 'sprotocore'
	print(core)

	print('00')
	local json = require 'cjson'
	print(json)
end
return cc